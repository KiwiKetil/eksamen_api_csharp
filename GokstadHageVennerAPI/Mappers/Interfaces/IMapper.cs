namespace StudentBloggAPI.Mappers.Interfaces;

public interface IMapper<TEntity, TDto> 
{                                      
    TDto MapToDTO(TEntity entity);
    TEntity MapToEntity(TDto dto);    
}
