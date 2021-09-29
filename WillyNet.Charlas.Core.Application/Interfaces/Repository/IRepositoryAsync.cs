using Ardalis.Specification;

namespace WillyNet.Charlas.Core.Application.Interfaces.Repository
{
    public interface IRepositoryAsync<T> : IRepositoryBase<T> where T : class { }    
    public interface IReadRepositoryAsync<T> : IReadRepositoryBase<T> where T : class { }
}
