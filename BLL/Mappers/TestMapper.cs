using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Mappers
{
    public static class TestMapper
    {
        public static TestDto MapToDto(this Test entity)
        {
            return new TestDto()
            {
                Id = entity.Id,
                Title = entity.Title,
                ThemeId = entity.ThemeId,
                ThemeTitle = entity.Theme.Title
            };
        }

        public static Test MapToEntity(this TestDto dto)
        {
            return new Test()
            {
                Id = dto.Id,
                Title = dto.Title,
                ThemeId = dto.ThemeId
            };
        }
    }
}
