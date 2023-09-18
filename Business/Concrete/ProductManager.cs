using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        
        ICategoryService _categoryService;
       

        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
           
            
            
        
        }
        [PerformanceAspect(5)]
        [ValidationAspect(typeof(ProductValidator))] 
        [SecuredOperation("product.add,admin")] 
        
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),CheckIfCategoryLimitExceeded());
            
            if (result!=null)
            {
                return result;
            }
            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);

            

            
            

            

            
        }
        [CacheAspect]
        //[SecuredOperation("product.add,admin")]
        public IDataResult<List<Product>> GetAll()
        {
            //İş Kodları

            if (DateTime.Now.Hour==25)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }
        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));

        }

        public IDataResult<List<ProductDetailDTO>> GetProductDetails()
        {

            if (DateTime.Now.Hour >= 22 && DateTime.Now.Hour<=24)
            {
                return new ErrorDataResult<List<ProductDetailDTO>>(Messages.MaintenanceTime);
            }

            

            return new SuccessDataResult<List<ProductDetailDTO>>(_productDal.GetProductDetails());

        }
        [ValidationAspect(typeof(ValidationAspect))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            throw new NotImplementedException();
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categporyId)
        {

            var result = _productDal.GetAll(p => p.CategoryId == categporyId).Count;
            if (result >= 20)
            {

                return new ErrorResult(Messages.ProductCountOfCategoryError);

            }

            return new SuccessResult();

        }

        private IResult CheckIfProductNameExists(string productName)
        {

            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {

                return new ErrorResult(Messages.ProductNameAlreadyExists);

            }

            return new SuccessResult();

        }

        private IResult CheckIfCategorynumber15(string productName)
        {

            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {

                return new ErrorResult(Messages.ProductNameAlreadyExists);

            }

            return new SuccessResult();

        }

        private IResult CheckIfCategoryLimitExceeded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceeded);
            }

            return new SuccessResult();
        }
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            _productDal.Update(product);
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
