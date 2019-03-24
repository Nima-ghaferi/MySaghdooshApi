using BE;
using BE.Entities.Requests;
using BE.Entities.Request;
using ErrorCenter;
using ErrorCenter.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BL.Business.Utility
{
    public class TokenUtils
    {
        private static readonly string encryptionKey = "ImLivingMyStar";
        private static readonly int Keysize = 256;
        private static readonly int DerivationIterations = 1001;

        public static Result<TokenResp> GetNewToken(ActivationCodeReq activationCode)
        {
            var tokenOperation = new TokenUtils(activationCode);
            return tokenOperation.GenerateTokenOperation();
        }

        public static bool CheckTokenValidation(string phoneNumber, string token)
        {
            return DA.Queries.UserAccounts.ValidateToken(phoneNumber, token);
        }

        private Result<TokenResp> GenerateTokenOperation()
        {
            Result<TokenResp> result;
            var tokenStr = GenerateToken(_activationCode);
            try
            {
                var user = DA.Queries.UserAccounts.SelectUserByTel(_activationCode.Tel);
                if (user == null)
                {
                    result = new Result<TokenResp>(null, new Error(ErrorMessage.TelIsNotValid), false);
                }
                else
                {
                    var tokenEntity = new NewTokenReq(user.Id, tokenStr, DateTime.Now);
                    DA.Queries.UserAccounts.InsertToken(tokenEntity);
                    result = new Result<TokenResp>(new TokenResp(tokenStr), null, true);
                }
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.Message, ex);
                result = new Result<TokenResp>(null, new Error(ErrorMessage.LoadUserByTelError), false);
            }
            
            return result;
        }

        private TokenUtils(ActivationCodeReq activationCode)
        {
            _activationCode = activationCode;
        }

        private ActivationCodeReq _activationCode;


        private string GenerateToken(ActivationCodeReq activationCode)
        {
            var clearText = activationCode.Code + activationCode.Tel + DateTime.Now.Ticks;
            var encryptedText = Encrypt(clearText);
            return encryptedText;
        }

        private string Encrypt(string clearText)
        {
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(clearText);
            using (var password = new Rfc2898DeriveBytes(encryptionKey, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }

        }

        public static string Decrypt(string cipherText)
        {
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(encryptionKey, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }
        
        private byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }
}
