using System.Linq.Expressions;

namespace LearningManagementSystem.Repositories.IRepository
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Trả về dòng dữ liệu tương ứng với Id nhập vào 
        /// </summary>
        /// <param name="id">Nhập vào id của entity</param>
        /// <returns>Biến kiểu Generic</returns>
        Task<T> GetById(int id);

        /// <summary>
        /// Trả về tất cả dòng dữ liệu
        /// </summary>
        /// <returns>Dữ liệu kiểu <see cref="IEnumerable{T}"/></returns>
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
        Task<bool> Add(T entity);
        Task<bool> AddRange(IEnumerable<T> entities);
        Task<bool> Remove(T entity);
        Task<bool> RemoveRange(IEnumerable<T> entities);
        Task<bool> Update(T entity);
    }
}
