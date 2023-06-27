﻿using AutoMapper;
using Business.Dtos;
using Business.Repositories.ParentRepository;
using Business.ViewModels;
using VillaEntity = DataAccess.Entities.Villa;

namespace Business.Repositories.VillaRepository
{
    internal class VillaRepository : Repository<VillaEntity>, IVillaRepository
    {
        private readonly IMapper _mapper;
        public VillaRepository(ApplicationDbContext dbContext , IMapper mapper) 
            : base(dbContext) => _mapper = mapper;

        public async Task<List<VillaViewModel>> GetAsync()
        {
            var entities = await base.GetAllAsync();

            var records = _mapper.Map<List<VillaViewModel>>(entities);

            return records; 
        }

        public void Insert(VillaCreateDto dto)
        {
            var entity = _mapper.Map<VillaEntity>(dto);

            ValidateEntity(entity , dto.Name);

            Insert(entity);
        }

        public async Task UpdateAsync(int id, VillaUpdateDto dto)
        {
            var entity = await GetAsync(id);

            await CheckIfVillaExist(id, dto.Name);

            var updatedEntity = _mapper.Map(dto , entity);

            ValidateEntity(updatedEntity, dto.Name); 

            Update(updatedEntity);
        }

        public async Task DeleteAsync(int? id)
        {
            await DoesIdExist(id ?? default);

            var entity = await GetAsync(id ?? default);

            Delete(entity);
        }



        private void ValidateEntity(VillaEntity entity , string name)
        {
            CheckNameAndDetailAreEmpty(entity.Name, entity.Detail);
            CheckNameAndDetailAreLetter(entity.Name, entity.Detail);
        }


        private async Task CheckIfVillaExist(int id , string name)
        {
            await DoesIdExist(id);

            var entity = await GetAsync(id);

            if (entity.Name == name)
                throw new Exception("Villa is already exist!");
        }
        private void CheckNameAndDetailAreLetter(string name , string detail)
        {
            if(int.TryParse(name, out _) 
                || int.TryParse(detail, out _))
                throw new Exception("Name or Detail is invalid");
        }
        private void CheckNameAndDetailAreEmpty(string name, string detail)
        {
            if(string.IsNullOrEmpty(name) 
                || string.IsNullOrEmpty(detail)) 
                throw new Exception("Name or Detail is empty");

        }
        private async Task DoesIdExist(int id)
        {
            var entity = await GetAsync(id);

            if(entity is null)
                throw new Exception("id does not exist");
        }
    }
}
