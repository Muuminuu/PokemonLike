using NuGet.Frameworks;
using PokemonLike.Models;
using PokemonLike.Models.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PokemonLike.Repositories.Tests
{

    public class BattleMonsterRepositoryTests
    {
        private BattleMonsterRepository _repo;

        public BattleMonsterRepositoryTests() 
        { 
            _repo = new BattleMonsterRepository();
        }

        [Fact]
        public void Get_Returns_BattleMonster()
        {
            var item = _repo.Get(1);
            Assert.NotNull(item);
        }

        [Fact]
        public void Get_Returns_Null()
        {
            var item = _repo.Get(-1);
            Assert.Null(item);
        }

        [Fact]
        public void GetAll_Returns_NotNull()
        {
            var items = _repo.GetAll();
            Assert.NotNull(items);
        }

        [Fact]
        public void GetAll_Returns_Items()
        {
            var items = _repo.GetAll();
            Assert.True(items?.Count > 0);
        }

        [Fact]
        public void Save_Adds_Item_And_Delete_Removes_It()
        {
            var newPokemon = new BattleMonster(0, "Test Pokemon", Models.MonsterType.Dragon, 100, 50, 50, 45);
            var newId = _repo.Save(newPokemon);
            Assert.True(newId > 0);
            Assert.True(_repo.Delete(newId));
        }

        [Fact]
        public void Save_Updates_Item()
        {
            // Get a pokemon the data source and ensure not null
            var existingPokemon = _repo.GetAll().OrderBy(p => p.Id).First();
            Assert.NotNull(existingPokemon);

            // cache name and create a new, random name
            var existingName = existingPokemon.Name;
            var newName = Guid.NewGuid().ToString();
            existingPokemon.Name = newName;

            // updating the entry
            var id = _repo.Save(existingPokemon);
            Assert.True(id == existingPokemon.Id);

            // verify updated work
            var copy = _repo.Get(id);
            Assert.NotNull(copy);
            Assert.True(copy.Name.Equals(newName));
            Assert.True(copy.Attack.Equals(existingPokemon.Attack));
            Assert.False(copy.Name.Equals(existingName));

            // resetting the entry to previous fields
            existingPokemon.Name = existingName;
            _repo.Save(existingPokemon);
            Assert.True(id == existingPokemon.Id);
        }

        [Fact]
        public void Save_Disallows_Update_When_False()
        {
            // Get a pokemon the data source and ensure not null
            var existingPokemon = _repo.GetAll().OrderBy(p => p.Id).First();
            Assert.NotNull(existingPokemon);

            // cache name and create a new, random name
            var existingName = existingPokemon.Name;
            var newName = Guid.NewGuid().ToString();
            existingPokemon.Name = newName;

            // updating the entry
            var id = _repo.Save(existingPokemon, false);
            Assert.True(id == -1);
        }

        [Fact]
        public void Delete_Returns_False_On_Fail() 
        {
            // pokemon id is greater than 0, so -1 __shouldn't__ exist
            Assert.False(_repo.Delete(-1));
        }
    }
}
