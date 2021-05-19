using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using DAL.Entities;

namespace BLL.Mappers
{
    public static class ThemeMapper
    {
        public static ThemeDto MapToDto(this Theme entity)
        {
            return new ThemeDto()
            {
                Id = entity.Id,
                Title = entity.Title,
            };
        }

        public static Theme MapToEntity(this ThemeDto dto)
        {
            return new Theme()
            {
                Id = dto.Id,
                Title = dto.Title,
            };
        }
    }
}
