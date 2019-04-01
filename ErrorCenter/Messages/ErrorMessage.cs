using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorCenter.Messages
{
    public class ErrorMessage
    {
        //DB ERRORS--Err00---------------------------------------------------------
        public static readonly string LoadCategoriesError = "Err00.001 خطا در بازیابی اطلاعات";
        public static readonly string AddNewServerInfoError = "Err00.002 خطا در ثبت اطلاعات";
        public static readonly string TokenStringIsNotValid = "Err00.003 توکن معنبر نمی باشد";
        public static readonly string TokenMobileIsNotValid = "Err00.004 توکن معنبر نمی باشد";
        public static readonly string TokenMobileIsNotValidAndMobileIsNotSame = "Err00.005 توکن معنبر نمی باشد";
        public static readonly string TokenValidationLogicFailed = "Err00.006 توکن معنبر نمی باشد";
        public static readonly string LoadActivationByUserError = "Err00.007 خطا در بازیابی اطلاعات";
        public static readonly string LoadUserByTelError = "Err00.008 خطا در بازیابی اطلاعات";
        public static readonly string AddNewUserError = "Err00.009 خطا در ثبت اطلاعات";
        public static readonly string AddNewActivationError = "Err00.010 خطا در ثبت اطلاعات";
        //USER ACCOUNT ERRORS--Err01-----------------------------------------------
        public static readonly string NewAccountNameIsEmpty = "Err01.001 یادت رفت اسمتو بگی";
        public static readonly string NewAccountTelIsEmpty = "Err01.002 یادت رفت شماره موبایلتو بگی";
        public static readonly string ActivationCodeIsNotValid = "Err01.003 کد فعال سازیت درست نیست";
        public static readonly string TelIsNotValid = "Err01.004 شماره موبایل درست نیست";
        public static readonly string UserRegisteredAlready = "Err01.005 تو که قبلا ثبت نام کردی!! کد فعال سازی جدید بگیر.";
        //SERVICE PROVIDERS ERRORS--Err02------------------------------------------
        public static readonly string LoadMainServiceProvidersError = "Err02.001 خطا در بازیابی اطلاعات";
        public static readonly string LoadCategoryServiceProvidersError = "Err02.002 خطا در بازیابی اطلاعات";
        public static readonly string LoadServiceProviderSpecsError = "Err02.003 خطا در بازیابی اطلاعات";
        public static readonly string ServiceProviderDoesNotExistError = "Err02.004 شناسه خدمات دهنده معتبر نیست";
        public static readonly string LikeServiceProviderError = "Err02.005 خطا در ثبت اطلاعات";
        public static readonly string DislikeServiceProviderError = "Err02.006 خطا در ثبت اطلاعات";

    }
}
