using System;
using StarterGame.Commands;
using StarterGame.Rooms;
using StarterGame.s;

namespace StarterGame
{
    public class Game
    {

        private Player _player;
        private Parser _parser;
        private Timer _timer;
        private bool _playing;

        public Game()
        {
            _player = new Player(CreateWorld());
            _parser = new Parser(new CommandWords());
            _timer = new Timer("Game", 180); //3 minutes in seconds
            _playing = false;
        }

        public Room CreateWorld()
        {
            Room frontporch = new Room("frontporch", "You are at the entrance of the Magnolia home on a relaxing covered porch!");
            Room foyer = new Room("foyer", "Welcome to the Foyer of this magical home!");
            Room study = new Room("study", "You are in the study which will make a lovely office!");
            Room familyroom = new Room("familyroom", "You are in the spacious family room!");
            Room kitchen = new Room("kitchen", "What a Spectacular kitchen!");
            Room diningarea = new Room("diningarea", "This dining area has so much natural light!");
            Room lefthall = new Room("lefthall", "You are in the hallway on the left side of the house!");
            Room righthall = new Room("righthall", "You are in the hallway on the right side of the house!");
            Room powderroom = new Room("powderroom", "Your guest will enjoy freshing up in this powder room!");
            Room primarybath = new Room("primarybath", "This Primary bathroom mimics a Spa!");
            Room primarycloset = new Room("primarycloset", "You are in the his and her closet of your dreams!");
            Room primarysuite = new Room("primarysuite", "This lavish suite is where you want to unwind everynight!");
            Room backporch = new Room("backporch", "What an Oasis this covered back porch is...!");
            Room flexroom = new Room("flexroom", "You can make this flex room a bedroom or a second living room, your Choice!");
            Room laundry = new Room("laundry", "Who would not enjoy doing laundry in here?");
            Room bath = new Room("bath", "This full bathroom is the perfect place to give your baby their first bath!");
            Room bedroomA = new Room("bedroomA", "What a cozy bedroom!");
            Room bedroomB = new Room("bedroomB", "A bedroom to remember!");
            Room transporterRoom = new Room("transporter", "A room that you can transport to any room without going in order");


            transporterRoom.SetExit(bedroomA);
            transporterRoom.SetExit(bedroomB);
            transporterRoom.SetExit(kitchen);
            transporterRoom.SetExit(primarycloset);
            transporterRoom.SetExit(primarysuite);
            transporterRoom.SetExit(backporch);
            transporterRoom.SetExit(flexroom);
            transporterRoom.SetExit(laundry);
            transporterRoom.SetExit(bath);
            transporterRoom.SetExit(diningarea);
            transporterRoom.SetExit(foyer);
            transporterRoom.SetExit(familyroom);

            Item dollarbill = new Item("drop off dollarbill");
            transporterRoom.Drop(dollarbill);

            //front door key unlocks the door between the frontporch and foyer
            Key frontdoorKey = new Key("key");
            //leaves key to be picked up in room
            frontporch.Drop(frontdoorKey);
            //sets exit to foyer where door is closed by default and requires key to open
            frontporch.SetExit(foyer, frontdoorKey);

            foyer.SetExit(study);
            foyer.SetExit(familyroom);

            //add booklet to familyroom
            Item booklet = new Item("booklet");
            //leaves booklet to be picked up in room
            familyroom.Drop(booklet);

            familyroom.SetExit(foyer);
            familyroom.SetExit(kitchen);
            familyroom.SetExit(diningarea);
            familyroom.SetExit(lefthall);

            lefthall.SetExit(powderroom);
            lefthall.SetExit(familyroom);
            lefthall.SetExit(primarybath);
            lefthall.SetExit(primarycloset);
            lefthall.SetExit(primarysuite);

            powderroom.SetExit(lefthall);
            primarybath.SetExit(lefthall);
            primarycloset.SetExit(lefthall);
            primarysuite.SetExit(lefthall);

            kitchen.SetExit(familyroom);
            kitchen.SetExit(diningarea);


            lefthall.SetExit(powderroom);
            lefthall.SetExit(familyroom);
            lefthall.SetExit(primarybath);
            lefthall.SetExit(primarycloset);
            lefthall.SetExit(primarysuite);

            powderroom.SetExit(lefthall);
            primarybath.SetExit(lefthall);
            primarycloset.SetExit(lefthall);
            primarysuite.SetExit(lefthall);


            ////Create and drop items in rooms
            IItem item = new Key("kettle", 1.5f, 2f);
            kitchen.Inspect(item);

            kitchen.SetExit(familyroom);
            kitchen.SetExit(diningarea);

            diningarea.SetExit(kitchen);
            diningarea.SetExit(familyroom);
            diningarea.SetExit(flexroom);

            // diningarea.SetExit("backporch", backporch);

            righthall.SetExit(flexroom);
            righthall.SetExit(laundry);
            righthall.SetExit(bath);
            righthall.SetExit(bedroomA);
            righthall.SetExit(bedroomB);

            flexroom.SetExit(righthall);
            laundry.SetExit(righthall);
            bath.SetExit(righthall);
            bedroomA.SetExit(righthall);
            bedroomB.SetExit(righthall);

            Item money = new Item("money");

            backporch.Drop(money);

            Key diningroomkey = new Key("key2");
            diningarea.Drop(diningroomkey);
            diningarea.SetExit(backporch, diningroomkey);

            //backporch.SetExit("diningarea", diningarea);

            return frontporch;
        }

        /**
         *  Main play routine. Loops until end of play.
         */
        public void Play()
        {

            // Enter the main command loop.  Here we repeatedly read commands and
            // execute them until the game is over.
            bool finished = false;

            _timer.Start();
            NotificationCenter.Instance.AddObserver("TimerFinished_Game", notification =>
            {
                finished = true;
                _player.OutputMessage("\n\nYOU RAN OUT OF TIME... YOU LOST\n\nWILL YOU COLLECT ALL OF THE MONEY NEXT TIME??");
            });



            while (!finished)
            {
                Console.Write("\n>");
                Command command = _parser.ParseCommand(Console.ReadLine());
                if (command == null)
                {
                    Console.WriteLine("I don't understand...");
                }
                else
                {
                    finished = command.Execute(_player);
                }
            }
        }


        public void Start()
        {
            _playing = true;
            _player.OutputMessage(Welcome());
            _timer.Reset();
        }

        public void End()
        {
            _playing = false;
            _player.OutputMessage(Goodbye());
            _timer.Pause();
        }

        public string Welcome()
        {
            //combines multiple line with the new line separator
            return string.Join("\n",
                "Welcome to The Magnolia Floor Plan!",
                "We are about to play a Real Estate viewing adventure game.",
                "",
                "Here is a list of commands available to you",
                _parser.Description(),
                "OBJECTIVE: You must find all of the money hidden in the rooms under 3 minutes!",
                "",
                _player.CurrentRoom.Description());
        }

        public string Goodbye()
        {
            return "\nThank you for playing, Goodbye.";
        }

    }
}