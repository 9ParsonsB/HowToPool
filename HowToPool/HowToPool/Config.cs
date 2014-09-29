using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HowToPool
{
    class Config
    {
        private State currentState = new State();
        public List<Entity> Entities { get; set; }
    }
}
