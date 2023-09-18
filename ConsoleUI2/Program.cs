using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;

public class Program
{
    static void Main(string[] args)
    {
        //ListtoCategory();
        //Console.WriteLine("------------------------------------------------------");
        ListtoProduct();
        Console.ReadLine();
    }

    private static void ListtoCategory()
    {
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        foreach (var category in categoryManager.GetAll().Data)
        {
            Console.WriteLine(category.CategoryName);
        }
    }

    private static void ListtoProduct()
    {
        ProductManager productManager = new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));

        var result = productManager.GetProductDetails();
        if (result.Success == true)
        {
            foreach (var product in productManager.GetProductDetails().Data)
            {
                Console.WriteLine(product.ProductName + "/" + product.CategoryName); ;
            }
            
        }
        else 
        {
            Console.WriteLine(result.Message);
        }

        
        
        Console.ReadLine();
    }
}