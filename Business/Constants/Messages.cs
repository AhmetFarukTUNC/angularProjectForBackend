using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {

        public static string ProductAdded = "Ürün eklendi";
        public static string ProductById = "İstenen id'ye sahip ürün getirildi.";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Bakım zamanı";
        public static string ProductListed = "Ürünler listelendi";

        public static string ProductCountOfCategoryError = "Max 10 ürün olabilir.";
        public static string ProductNameAlreadyExists = "Bu üründen var zaten!";

        public static string CategoryLimitExceeded = "Kategori limiti aşıldı.";

        public static string AuthorizationDenied = "Yetkiniz yok";
        public static string UserRegistered = "Kayıt başarılı!";
        internal static string UserNotFound = "Emaile sahip kullanıcı yok!";
        internal static string PasswordError = "Şifreniz yanlış!";
        internal static string SuccessfulLogin ="Giriş Başarılı";
        internal static string UserAlreadyExists ="Zaten bu kullanıcı var!";
        internal static string AccessTokenCreated = "Access token oluşturuldu";
        internal static string ProductUpdated = "Ürün eklendi!";
    }
}
