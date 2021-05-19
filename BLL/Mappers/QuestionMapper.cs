using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Mappers
{
    public static class QuestionMapper
    {
        public static QuestionDto MapToDto(this Question entity)
        {
            return new QuestionDto()
            {
                Id = entity.Id,
                TaskText = entity.TaskText,
                Score = entity.Score,
                TestId = entity.TestId,
                TestTitle = entity.Test.Title
            };
        }

        public static Question MapToEntity(this QuestionDto dto)
        {
            return new Question()
            {
                Id = dto.Id,
                TaskText = dto.TaskText,
                Score = dto.Score,
                TestId = dto.TestId,
            };
        }
    }
}
