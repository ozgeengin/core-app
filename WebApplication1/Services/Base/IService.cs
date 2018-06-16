using System.Collections.Generic;
using WebApplication1.Dtos;

namespace WebApplication1.Services.Base
{
    public interface IService<TDto>
        where TDto : BaseDto
    {
        Result<int> Insert(TDto dto);
        Result<int> Insert(IEnumerable<TDto> dtoList);
        Result<int> Update(TDto dto);
        Result<int> Update(IEnumerable<TDto> dtoList);
        Result<int> Delete(TDto dto);
        Result<int> Delete(int id);
        Result<int> Delete(IEnumerable<TDto> dtoList);
        Result<int> Delete(IEnumerable<int> idList);
        Result<List<TDto>> GetAll();
        Result<TDto> GetById(int id);
    }
}
