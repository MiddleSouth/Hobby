using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RLSimulation.Logic
{
    public class MazeService
    {

        public Task<Maze> GetMazeAsync()
        {
            return Task.FromResult(new Maze());
        }
    }
}
