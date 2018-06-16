using System;
using System.Collections.Generic;
using System.Diagnostics;
using AutoMapper;
using WebApplication1.Data.Base;
using WebApplication1.Dtos;
using WebApplication1.Entities;

namespace WebApplication1.Services.Base
{
    public class BaseService<TEntity, TDto,TRepository> : IService<TDto>
        where TEntity : BaseEntity
        where TDto : BaseDto
        where TRepository : IBaseRepository<TEntity>
    {
        protected TRepository Repository;
        private readonly IMapper _mapper;

        public BaseService(TRepository repository, IMapper mapper) {
            Repository = repository;
            _mapper = mapper;
        }
        
        public TDto MapToDto(TEntity entity) => _mapper.Map<TEntity, TDto>(entity);

        public TEntity MapToEntity(TDto dto) => _mapper.Map<TDto, TEntity>(dto);

        public List<TDto> MapDtoList(List<TEntity> entities)
        {
            var result = new List<TDto>();
            if (entities == null) return result;
            foreach (var entity in entities)
                result.Add(_mapper.Map<TEntity, TDto>(entity));
            return result;
        }

        public List<TEntity> MapDtoList(List<TDto> dtos)
        {
            var result = new List<TEntity>();
            if (dtos == null) return result;
            foreach (var dto in dtos)
                result.Add(_mapper.Map<TDto, TEntity>(dto));
            return result;
        }

        private void BaseTransaction(Func<List<TDto>, List<TDto>> func)
        {
            using (var dbContextTransaction = Repository.BaseContext.Database.BeginTransaction())
            {
                var isCommit = true;
                try
                {
                    func(new List<TDto>());
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(string.Concat(ex.Message, ",", ex.Data));
                    dbContextTransaction.Rollback();
                    isCommit = false;
                }
                if (isCommit)
                    dbContextTransaction.Commit();
            }
        }

        public Result<int> Insert(TDto dto)
        {
            return new Result<int>(Repository.Insert(MapToEntity(dto)));
        }

        public Result<int> Update(TDto dto)
        {
            return new Result<int>(Repository.Update(MapToEntity(dto)));
        }

        public Result<int> Delete(TDto dto)
        {
            return new Result<int>(Repository.DeleteById(dto.Id));
        }

        public Result<int> Delete(int id)
        {
            return new Result<int>(Repository.DeleteById(id));
        }

        public Result<int> Insert(IEnumerable<TDto> dtoList)
        {
            var result = 0;
            BaseTransaction(x =>
            {
                foreach (var dto in dtoList)
                    Repository.InsertWithoutSave(MapToEntity(dto));

                result = Repository.Save();
                return x;
            });
            return new Result<int>(result);
        }

        public Result<int> Update(IEnumerable<TDto> dtoList)
        {
            var result = 0;
            BaseTransaction(x =>
            {
                foreach (var dto in dtoList)
                    Repository.UpdateWithoutSave(MapToEntity(dto));

                result = Repository.Save();
                return x;
            });
            return new Result<int>(result);
        }

        public Result<int> Delete(IEnumerable<TDto> dtoList)
        {
            var result = 0;
           BaseTransaction(x =>
            {
                foreach (var dto in dtoList)
                    Repository.DeleteByIdWithoutSave(dto.Id);

                result = Repository.Save();
                return x;
            });
            return new Result<int>(result);
        }

        public Result<int> Delete(IEnumerable<int> idList)
        {
            var result = 0;
            BaseTransaction(x =>
            {
                foreach (var id in idList)
                    Repository.DeleteByIdWithoutSave(id);

                result = Repository.Save();
                return x;
            });
            return new Result<int>(result);
        }


        public Result<List<TDto>> GetAll()
        {
            return new Result<List<TDto>>(MapDtoList(Repository.GetAll()));
        }

        public Result<TDto> GetById(int id)
        {
            return new Result<TDto>(MapToDto(Repository.GetById(id)));
        }
    }
}
