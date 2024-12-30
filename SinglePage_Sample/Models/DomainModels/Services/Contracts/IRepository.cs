﻿using SinglePage_Sample.Frameworks.ResponseFrameworks.Contracts;

namespace SinglePage_Sample.Models.DomainModels.Services.Contracts
{
    public interface IRepository<T,TCollection>
    {
        Task<IResponse<TCollection>> SelectAll();
        Task<IResponse<T>> Select(T obj);
        Task<IResponse<T>> Insert(T obj);
        Task<IResponse<T>> Update(T obj);
        Task<IResponse<T>> Delete(T obj);
    }
}
