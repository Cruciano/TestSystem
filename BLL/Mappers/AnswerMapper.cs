using BLL.DTO;
using DAL.Entities;

namespace BLL.Mappers
{
    public static class AnswerMapper
    {
        public static AnswerDto MapToDto(this Answer entity)
        {
            return new AnswerDto()
            {
                Id = entity.Id,
                Text = entity.Text,
                IsCorrect = entity.IsCorrect,
                QuestionId = entity.QuestionId
            };
        }

        public static Answer MapToEntity(this AnswerDto dto)
        {
            return new Answer()
            {
                Id = dto.Id,
                Text = dto.Text,
                IsCorrect = dto.IsCorrect,
                QuestionId = dto.QuestionId
            };
        }
    }
}
