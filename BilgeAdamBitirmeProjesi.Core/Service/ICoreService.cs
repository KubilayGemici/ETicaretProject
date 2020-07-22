using BilgeAdamBitirmeProjesi.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.Core.Service
{
    //Repository database ile ilgili işlemleri yaparlar.
    //Businesslar Servislerde olur.Bir business birden fazla database bağlanabilir.
    public interface ICoreService<T> where T : CoreEntity
    {
        //TASK => Bu metot ayrı bir iş parçacığı üzerinde çalışmasını sağlarız.Birden fazla tetiklenebilmesi için.Aynı anda birden fazla iş parçacığına bölünebilmesini sağlarım.
        Task<T> Add(T item);
        //Tek tek içerisinde dönüp geriye bool döndürücem.
        Task<bool> AddRange(List<T> items);
        Task<T> Update(T item);
        Task<bool> Remove(T item);
        //İd verdiğime true veya false dönsün.
        Task<bool> Remove(Guid id);
        //Sorguya göre burası bool döndürüyorsa , dönen cevaba göre hepsini silsin.
        Task<bool> RemoveAll(Expression<Func<T, bool>> exp);
        //Params virgül ile ayırabilmek için kullanıyorum.
        Task<T> GetById(Guid id, params Expression<Func<T,object>>[] includeParameters);
        Task<T> GetByDefault(Expression<Func<T, bool>> exp, params Expression<Func<T,object>>[] includeParameters);
        //IQueryable => To List demeyene kadar ekleme yapar.(Sorgu oluşturucu)
        IQueryable<T> GetActive();
        IQueryable<T> Default(Expression<Func<T, bool>> exp, params Expression<Func<T, object>>[] includeParameters);
        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
        Task<bool> Activate(Guid id);
        Task<bool> Any(Expression<Func<T, bool>> exp);
        Task<int> Save();

    }
}
