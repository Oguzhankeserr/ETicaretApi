//using ETicaretAPI.Application.Abstractions.Storage.FireBase;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Threading.Tasks;
//using Firebase.Storage;

//namespace ETicaretAPI.Infrastructure.Services.Storage.FireBase
//{
//    public class FireBaseStorage : Storage, IFireBaseStorage
//    {
//        private readonly FirebaseStorage _fireBaseStorage;

//        public FireBaseStorage(string firebaseStorageBucket)
//        {
//            _fireBaseStorage = new FirebaseStorage(firebaseStorageBucket);
//        }

//        public async Task DeleteAsync(string pathOrContainerName, string fileName)
//        {
//            // Firebase Storage'da silme işlemi
//            await _fireBaseStorage.Child(pathOrContainerName).Child(fileName).DeleteAsync();
//        }


//        public new List<string> GetFiles(string pathOrContainerName)
//        {
//            var metaData = _fireBaseStorage.Child(pathOrContainerName).GetMetaDataAsync().Result;
//            return new List<string> { metaData.Name };
//        }

//        public bool HasFile(string pathOrContainerName, string fileName)
//        {
//            // Firebase Storage'da dosyanın varlığını kontrol etme işlemi
//            var reference = _fireBaseStorage.Child(pathOrContainerName).Child(fileName);
//            return reference.GetDownloadUrlAsync().Result != null;
//        }
//        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
//        {
//            var firebasePath = $"{pathOrContainerName}/";
//            var datas = new List<(string fileName, string pathOrContainerName)>();

//            foreach (var file in files)
//            {
//                var fileNewName = await FileRenameAsync(pathOrContainerName, file.Name, HasFile);
//                var firebaseFileName = $"{firebasePath}{fileNewName}";

//                using (var stream = file.OpenReadStream())
//                {
//                    // Firebase Storage'a dosya yükleme işlemi
//                    await _fireBaseStorage.Child(firebaseFileName).PutAsync(stream);
//                }

//                datas.Add((fileNewName, firebaseFileName));
//            }

//            return datas;
//        }
//    }
//}