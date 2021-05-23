using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Mappers;
using BLL.DTO;
using DAL.Interfaces;
using DAL.Entities;

namespace BLL.Services
{
    public class ThemeService : IThemeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ThemeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<ThemeDto>> GetAllAsync()
        {
            List<ThemeDto> themes = new();
            IEnumerable<Theme> themeEntities = await _unitOfWork.ThemeRepository.GetAllAsync();

            foreach (var entity in themeEntities)
            {
                themes.Add(entity.MapToDto());
            }

            foreach (var theme in themes)
            {
                List<TestDto> tests = new();
                IEnumerable<Test> testEntities = await _unitOfWork.TestRepository
                    .GetByConditionAsync(test => test.Id == theme.Id);

                foreach (var entity in testEntities)
                {
                    tests.Add(entity.MapToDto());
                }

                theme.Tests = tests;
            }

            return themes;
        }

        public async Task<ThemeDto> GetByIdAsync(int id)
        {
            Theme themeEntity = await _unitOfWork.ThemeRepository.ReadAsync(id);
            if (themeEntity == null)
                throw new Exception("Nothing was found by this Id");

            ThemeDto theme = themeEntity.MapToDto();

            List<TestDto> tests = new();
            IEnumerable<Test> testEntities = await _unitOfWork.TestRepository
                .GetByConditionAsync(test => test.ThemeId == themeEntity.Id);

            foreach (var entity in testEntities)
            {
                tests.Add(entity.MapToDto());
            }

            theme.Tests = tests;
            return theme;
        }

        public async Task CreateAsync(ThemeDto dto)
        {
            await _unitOfWork.ThemeRepository.CreateAsync(dto.MapToEntity());
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(ThemeDto dto)
        {
            _unitOfWork.ThemeRepository.Update(dto.MapToEntity());
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Theme themeEntity = await _unitOfWork.ThemeRepository.ReadAsync(id);
            if (themeEntity == null)
                throw new Exception("Nothing was found by this Id");

            _unitOfWork.ThemeRepository.Delete(themeEntity);
            await _unitOfWork.SaveAsync();
        }
    }
}
