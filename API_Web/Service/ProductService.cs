using API_Web.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Web.Service
{
    public class ProductService : IProductService
    {
        private readonly ModelDbContext _db;
        public ProductService(ModelDbContext db)
        {
            _db = db;
        }
        public async Task<Products> Create(Products products)
        {
            products.CreatedDate = DateTime.Now;
            _db.products.Add(products);
            await _db.SaveChangesAsync();
            return products;
        }
        public async Task<bool> Delete(int id)
        {
            var finid = _db.products.Find(id);
            if (finid != null)
            {
                _db.products.Remove(finid);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }
        
        public async Task<IEnumerable<Products>> GetAll()
        {
            return await _db.products.ToListAsync();
        }

        public async Task<Products> GetById(int id)
        {
            return await _db.products.FindAsync(id);
        }

        public async Task<Products> Update(Products employess)
        {
            _db.products.Update(employess);
            await _db.SaveChangesAsync();
            return employess;
        }
    }
}
