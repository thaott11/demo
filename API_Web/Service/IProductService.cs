using API_Web.Models;

namespace API_Web.Service
{
    public interface IProductService
    {
        Task<IEnumerable<Products>> GetAll();
        Task<Products> GetById(int id);
        Task<Products> Create(Products employess);
        Task<Products> Update(Products employess);
        Task<bool> Delete(int id);
    }
}
