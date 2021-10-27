using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino
{
    public abstract class Game //this is called "Super Class ".
    {

    private List<Player> _players = new List<Player>();
    private Dictionary<Player, int> _bets = new Dictionary<Player, int>();

    public List<Player> Players { get { return _players; } set { _players = value; } }
    //Properties:
    
    public string Name { get; set; }
    public Dictionary<Player, int>  Bets { get { return _bets; } set { _bets = value; } }


        //Methods:

        public virtual void ListPlayers()//virtual method inside abstract class 
            //means this method gets inherited by an inheriting class but can be
            //overwritten by inheriting class. They have implementation but 
            //can be overwritten. 
        {
            foreach (Player player in Players )
            {
                Console.WriteLine(player.Name);
            }
        }

     public abstract void Play();//can only be in an abstract class. Contains
        //NO implementation. Any class inheriting from Game MUST have this play method. 
        //It must have this method. 


    }
}
