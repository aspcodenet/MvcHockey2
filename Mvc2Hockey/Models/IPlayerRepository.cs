using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mvc2Hockey.Models
{
    public interface IPlayerRepository
    {
        IEnumerable<Player> GetAll();
        Player Get(int id);
    }

    class PlayerRepository : IPlayerRepository
    {
        public IEnumerable<Player> GetAll()
        {
            return new[]
            {
                new Player{ Id = 1, JerseyNumber = 13, Name = "Mats Sundin"},
                new Player{ Id = 2, JerseyNumber = 21, Name = "Peter Forsberg"},
                new Player{ Id = 3, JerseyNumber = 5, Name = "Niklas Lidström"}
            };
        }

        public Player Get(int id)
        {
            return GetAll().FirstOrDefault(p => p.Id == id);
        }
    }
}