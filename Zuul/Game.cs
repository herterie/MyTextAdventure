﻿using System;

namespace Zuul
{
	public class Game
	{
		private Parser parser;
		private Player player;

		//items
		private Item key;
		private Item knife;

		public Game ()
		{
			parser = new Parser();
			player = new Player();
			CreateRooms();
		}

		private void CreateRooms()
		{
			// create the rooms
			Room outside = new Room("outside the main entrance of the university");
			Room theatre = new Room("in a lecture theatre");
			Room pub = new Room("in the campus pub");
			Room lab = new Room("in a computing lab");
			Room office = new Room("in the computing admin office");
			Room well = new Room("in a dry well");

			// initialise room exits
			outside.AddExit("east", theatre);
			outside.AddExit("south", lab);
			outside.AddExit("west", pub);
			outside.AddExit("down", well);

			well.AddExit("up", outside);

			theatre.AddExit("west", outside);

			pub.AddExit("east", outside);

			lab.AddExit("north", outside);
			lab.AddExit("east", office);

			office.AddExit("west", lab);

			//items
			key = new Item(2, "A old key");
			knife = new Item(5, "A razor sharp knife");

			outside.addItems("key",key);

			lab.addItems("knife", knife);

			player.CurrentRoom = outside;  // start game outside
		}
		/**
		 *  Main play routine.  Loops until end of play.
		 */
		public void Play()
		{
			PrintWelcome();

			// Enter the main command loop.  Here we repeatedly read commands and
			// execute them until the player wants to quit.
			bool finished = false;
			while (!finished)
			{
				Command command = parser.GetCommand();
				finished = ProcessCommand(command);
			}
			Console.WriteLine("Thank you for playing.");
			Console.WriteLine("Press [Enter] to continue.");
			Console.ReadLine();
		}

		/**
		 * Print out the opening message for the player.
		 */
		private void PrintWelcome()
		{
			Console.WriteLine();
			Console.WriteLine("Welcome to Zuul!");
			Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
			Console.WriteLine("Type 'help' if you need help.");
			Console.WriteLine();
			Console.WriteLine(player.CurrentRoom.GetLongDescription());
			player.CurrentRoom.items();
		}

		/**
		 * Given a command, process (that is: execute) the command.
		 * If this command ends the game, true is returned, otherwise false is
		 * returned.
		 */
		private bool ProcessCommand(Command command)
		{
			bool wantToQuit = false;

			if(command.IsUnknown())
			{
				Console.WriteLine("I don't know what you mean...");
				return false;
			}

			string commandWord = command.GetCommandWord();
			switch (commandWord)
			{
				case "help":
					PrintHelp();
					break;
				case "go":
					GoRoom(command);
					break;
				case "quit":
					wantToQuit = true;
					break;
				case "look":
					lookAround();
                    break;
				case "items":
					myItems();
					break;
				case "take":
					Take(command);
					break;
				case "drop":
					Drop(command);
					break;
			}

            return wantToQuit;
		}

		private void Take(Command command)
		{
			if (!command.HasSecondWord())
			{
				// if there is no second word, we don't know which item..
				Console.WriteLine("Which?");
			}

			string nameItem = command.GetSecondWord();

			player.TakeFromChest(nameItem);

		}
		private void Drop(Command command)
		{
			if (!command.HasSecondWord())
			{
				// if there is no second word, we don't know which item..
				Console.WriteLine("Which?");
			}

			string nameItem = command.GetSecondWord();

			player.DropToChest(nameItem);
		}

		private void myItems()
        {
			player.items();

		}
		private void lookAround()
		{
			Console.WriteLine(player.CurrentRoom.GetLongDescription());
			player.CurrentRoom.items();
		}
		// implementations of user commands:

		/**
		 * Print out some help information.
		 * Here we print the mission and a list of the command words.
		 */
		private void PrintHelp()
		{
			Console.WriteLine("You are lost. You are alone.");
			Console.WriteLine("You wander around at the university.");
			Console.WriteLine();
			// let the parser print the commands
			parser.PrintValidCommands();
		}
		
		/**
		 * Try to go to one direction. If there is an exit, enter the new
		 * room, otherwise print an error message.
		 */
		private void GoRoom(Command command)
		{
			if(!command.HasSecondWord())
			{
				// if there is no second word, we don't know where to go...
				Console.WriteLine("Go where?");
				return;
			}

			string direction = command.GetSecondWord();

			// Try to go to the next room.
			Room nextRoom = player.CurrentRoom.GetExit(direction);

			if (nextRoom == null)
			{
				Console.WriteLine("There is no door to "+direction+"!");
			}
			else
			{
				player.CurrentRoom = nextRoom;
				Console.WriteLine(player.CurrentRoom.GetLongDescription());
				player.Damage(1);
				player.CurrentRoom.items();
			}
		}

	}
}
