using Data.Models;
using Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Service.IServices;

namespace Service.Services
{
     public class CompanyProfileService: ICompanyProfileService
    {
        private IUnitOfWork db;
        public CompanyProfileService(IUnitOfWork db)
        {
            this.db = db;
        }
        public void Update(CompanyProfile model)
        {
            db.CompanyProfile.Update(model);
        }
        public CompanyProfile Add(CompanyProfile model)
        {
            db.CompanyProfile.Insert(model);
            return model;
        }
        public CompanyProfile GetById(long id)
        {
            return db.CompanyProfile.Get().Where(x => x.ID == id).FirstOrDefault();
           
        }
        public IEnumerable<CompanyProfile> GetAll()
        {
            return db.CompanyProfile.GetAll();
        }

        public CustImage AddImage(CustImage image)
        {
            var check = CheckImage(image);

            if(check != null && check.Id > 0)
            {
                check.ImagePath = image.ImagePath;
                db.CustImage.Update(check);
                
            }
            else
            {
                db.CustImage.Insert(image);
            }
            
            return image;
        }

        public CustImage CheckImage(CustImage image)
        {
            var checkedImage = db.CustImage.Get().Where(x => x.ImageType == image.ImageType).FirstOrDefault();

            return checkedImage;
        }

        public string GetImageByTypeId(int type)
        {
            try
            {   
            var Image = db.CustImage.Get().Where(x => x.ImageType == type).FirstOrDefault();
            string base64Image = Convert.ToBase64String(Image.ImagePath);
            return base64Image;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

    }
}
