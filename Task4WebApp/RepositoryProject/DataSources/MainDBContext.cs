using System;
using System.Collections.Generic;
using DALProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DALProject
{
	public class MainDBContext : DbContext
	{
		public MainDBContext()
		{
			Database.EnsureCreated();
		}

		public DbSet<Flight> Flights { get; set; }
		public DbSet<Departure> Departures { get; set; }
		public DbSet<Stewardess> Stewardesses { get; set; }
		public DbSet<Pilot> Pilots { get; set; }
		public DbSet<PlaneType> PlaneTypes { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MyTestDatabase;Trusted_Connection=True;Integrated Security=True;", b => b.MigrationsAssembly("Task4WebApp"));
			optionsBuilder.EnableSensitiveDataLogging(true);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//modelBuilder.Entity<Flight>()
			//	.HasMany<Ticket>(f => f.Tickets)
			//	.WithOne("CurrentFlight")
			//	.HasForeignKey(t => t.Id)
			//	.OnDelete(DeleteBehavior.Cascade);




			modelBuilder.Entity<Pilot>().HasData(
				new Pilot { Id = 1, Name = "Adam", Surname = "Benn", BirthDate = DateTime.Parse("1978-11-01"), Experience = DateTime.Today - DateTime.Parse("2004-12-01") },
				new Pilot { Id = 2, Name = "Max", Surname = "Payne", BirthDate = DateTime.Parse("1964-06-12"), Experience = DateTime.Today - DateTime.Parse("1987-03-24") },
				new Pilot { Id = 3, Name = "Francis", Surname = "Castle", BirthDate = DateTime.Parse("1974-02-01"), Experience = DateTime.Today - DateTime.Parse("1992-04-30") }
				);
			modelBuilder.Entity<Stewardess>().HasData(
				new Stewardess { Id = 1, Name = "Eve", Surname = "Fairy", BirthDate = DateTime.Parse("2001-06-12"), CrewId = 1 },
				new Stewardess { Id = 2, Name = "Samantha", Surname = "Simson", BirthDate = DateTime.Parse("1999-12-03"), CrewId = 1 },
				new Stewardess { Id = 3, Name = "Gabrial", Surname = "Fate", BirthDate = DateTime.Parse("1998-05-13"), CrewId = 2 },
				new Stewardess { Id = 4, Name = "Hannah", Surname = "Screw", BirthDate = DateTime.Parse("1993-07-08"), CrewId = 3 },
				new Stewardess { Id = 5, Name = "Jennah", Surname = "Johns", BirthDate = DateTime.Parse("1996-04-03"), CrewId = 2 },
				new Stewardess { Id = 6, Name = "Ivory", Surname = "Rocket", BirthDate = DateTime.Parse("1992-08-19"), CrewId = 3 }
				);
			modelBuilder.Entity<PlaneType>().HasData(
				new PlaneType { Id = 1, AirLift = 58100, Model = "Boeing 737-200", Seats = 130 },
				new PlaneType { Id = 2, AirLift = 244900, Model = "Boeing 787 Dreamliner", Seats = 250 },
				new PlaneType { Id = 3, AirLift = 351800, Model = "Boeing 777-300", Seats = 550 }
				);
			modelBuilder.Entity<Ticket>().HasData(

														new Ticket { Id = 1, Price = 25.00, FlightId = 1 },
														new Ticket { Id = 2, Price = 35.00, FlightId = 1 },
														new Ticket { Id = 3, Price = 45.00, FlightId = 1 },

														new Ticket { Id = 4, Price = 25.00, FlightId = 2 },
														new Ticket { Id = 5, Price = 35.00, FlightId = 2 },
														new Ticket { Id = 6, Price = 45.00, FlightId = 2 },


														new Ticket { Id = 7, Price = 25.00, FlightId = 3 },
														new Ticket { Id = 8, Price = 35.00, FlightId = 3 },
														new Ticket { Id = 9, Price = 45.00, FlightId = 3 },

														new Ticket { Id = 10, Price = 25.00, FlightId = 4 },
														new Ticket { Id = 11, Price = 35.00, FlightId = 4 },
														new Ticket { Id = 12, Price = 45.00, FlightId = 4 },

														new Ticket { Id = 13, Price = 25.00, FlightId = 5 },
														new Ticket { Id = 15, Price = 35.00, FlightId = 5 },
														new Ticket { Id = 16, Price = 45.00, FlightId = 5 },


														new Ticket { Id = 17, Price = 25.00, FlightId = 6 },
														new Ticket { Id = 18, Price = 35.00, FlightId = 6 },
														new Ticket { Id = 19, Price = 45.00, FlightId = 6 }

					);
			modelBuilder.Entity<Flight>().HasData(
				new Flight
				{
					Id = 1,
					DeparturePoint = "Rome",
					Destination = "Paris",
					DepartureTime = DateTime.Parse("2018-01-11T15:45:00"),
					ArrivalTime = DateTime.Parse("2018-01-11T15:45:00"),

				},
				new Flight
				{
					Id = 2,
					DeparturePoint = "NY",
					Destination = "Paris",
					DepartureTime = DateTime.Parse("2018-01-11T15:45:00"),
					ArrivalTime = DateTime.Parse("2018-01-11T15:45:00"),

				},
				new Flight
				{
					Id = 3,
					DeparturePoint = "Ottawa",
					Destination = "Paris",
					DepartureTime = DateTime.Parse("2018-01-11T15:45:00"),
					ArrivalTime = DateTime.Parse("2018-01-11T15:45:00"),

				},
				new Flight
				{
					Id = 4,
					DeparturePoint = "Rome",
					Destination = "Paris",
					DepartureTime = DateTime.Parse("2018-01-11T15:45:00"),
					ArrivalTime = DateTime.Parse("2018-01-11T15:45:00"),

				},
				new Flight
				{
					Id = 5,
					DeparturePoint = "NY",
					Destination = "Paris",
					DepartureTime = DateTime.Parse("2018-01-11T15:45:00"),
					ArrivalTime = DateTime.Parse("2018-01-11T15:45:00"),

				},
				new Flight
				{
					Id = 6,
					DeparturePoint = "Ottawa",
					Destination = "Paris",
					DepartureTime = DateTime.Parse("2018-01-11T15:45:00"),
					ArrivalTime = DateTime.Parse("2018-01-11T15:45:00"),

				});

			modelBuilder.Entity<Crew>().HasData(
					new Crew { Id = 1, PilotId = 1, },
					new Crew { Id = 2, PilotId = 2, },
					new Crew { Id = 3, PilotId = 3, }
					);
			

			modelBuilder.Entity<Plane>().HasData(
				new Plane
				{
					Id = 1,
					Name = "Boeing 787",
					TypeId=1,
					ReleaseDate = DateTime.Parse("2010-10-15"),
					OperationLife = DateTime.Parse("2022-10-15") - DateTime.Parse("2010-10-15")
				},
				new Plane
				{
					Id = 2,
					Name = "Boeing 737",
					TypeId=2,
					ReleaseDate = DateTime.Parse("2013-02-01"),
					OperationLife = DateTime.Parse("2026-02-01") - DateTime.Parse("2013-02-01")
				},
				new Plane
				{
					Id = 3,
					Name = "Boeing 777",
					TypeId = 3,
					ReleaseDate = DateTime.Parse("2012-09-01"),
					OperationLife = DateTime.Parse("2028-09-01") - DateTime.Parse("2012-09-01")
				}
				);

			modelBuilder.Entity<Departure>().HasData(
		new Departure
		{
			Id = 1,
			FlightId = 2,
			DepartureDate = DateTime.Parse("2018-01-11T15:45:00"),
			PlaneItemId = 3,
			CrewItemId = 1
		},
		new Departure
		{
			Id = 2,
			FlightId = 3,
			DepartureDate = DateTime.Parse("2018-02-13T11:30:00"),
			PlaneItemId=2,
			CrewItemId = 3
		},
		new Departure
		{
			Id = 3,
			FlightId = 5,
			DepartureDate = DateTime.Parse("2018-01-11T15:45:00"),
			PlaneItemId=1,
			CrewItemId=2
			
		}
			);

		}


	}
}
