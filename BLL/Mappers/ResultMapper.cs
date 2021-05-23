using BLL.DTO;
using DAL.Entities;


namespace BLL.Mappers
{
    public static class ResultMapper
    {
        public static ResultDto MapToDto(this Result entity)
        {
            return new ResultDto()
            {
                Id = entity.Id,
                Score = entity.Score,
                DateTime = entity.DateTime,
                TestTitle = entity.TestTitle,
                UserName = entity.user.FirstName,
                UserId = entity.UserId,
                TestId = entity.TestId
            };
        }

        public static Result MapToEntity(this ResultDto dto)
        {
            return new Result()
            {
                Id = dto.Id,
                Score = dto.Score,
                DateTime = dto.DateTime,
                TestTitle = dto.TestTitle,
                UserId = dto.UserId,
                TestId = dto.TestId
            };
        }
    }
}
