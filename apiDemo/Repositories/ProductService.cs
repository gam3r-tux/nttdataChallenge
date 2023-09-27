using apiDemo.Data;
using apiDemo.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace apiDemo.Repositories
{
    public class ProductService : IProductService
    {
    private readonly DbContextClass _dbContext;
    public ProductService(DbContextClass dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Product>> GetProductListAsync()
    {
        return await _dbContext.Product.FromSqlRaw<Product>("GetPrductList").ToListAsync();
    }
    public async Task<IEnumerable<Product>> GetProductByIdAsync(int ProductId)
    {
        var param = new SqlParameter("@ProductId", ProductId);
        var productDetails = await Task.Run(() => _dbContext.Product.
        FromSqlRaw(@"exec GetPrductByID @ProductId", param).ToListAsync());
        return productDetails;
    }
    public async Task<int> AddProductAsync(Product product)
    {
        var parameter = new List<SqlParameter>();
        parameter.Add(new SqlParameter("@Name", product.Name));
        parameter.Add(new SqlParameter("@UnitPrice", product.UnitPrice));
        parameter.Add(new SqlParameter("@Cost", product.Cost));
        parameter.Add(new SqlParameter("@Active", product.Active));
        var result = await Task.Run(() => _dbContext.Database.
        ExecuteSqlRawAsync(@"exec AddNewProduct @Name, @UnitPrice, @Cost, @Active", parameter.ToArray()));
        return result;
    }
    public async Task<int> UpdateProductAsync(Product product)
    {
        var parameter = new List<SqlParameter>();
        parameter.Add(new SqlParameter("@ProductId", product.ProductId));
        parameter.Add(new SqlParameter("@Name", product.Name));
        parameter.Add(new SqlParameter("@UnitPrice", product.UnitPrice));
        parameter.Add(new SqlParameter("@Cost", product.Cost));
        parameter.Add(new SqlParameter("@Active", product.Active));
        var result = await Task.Run(() => _dbContext.Database.
        ExecuteSqlRawAsync(@"exec UpdateProduct @ProductId, @Name, @UnitPrice, @Cost, @Active", parameter.ToArray()));
        return result;
    }
    public async Task<int> DeleteProductAsync(int ProductId)
    {
        return await Task.Run(() => _dbContext.Database.ExecuteSqlInterpolatedAsync($"DeletePrductByID {ProductId}"));
    }
    }
}

