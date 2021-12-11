# My solution

## How to run
1. Clone this repository like `git clone https://github.com/04ea5aa4/120e5c5c.git`
2. You will need to [download .NET](https://dotnet.microsoft.com/en-us/download) to build and run my API.
3. Navigate to the project directory like `cd LinkPage`
4. Run `dotnet run` to restore dependencies, build, and run the solution!

## Decisions I made
* My solution is built with .NET 6 and ASP Core because that's what I am most familiar with.
* I decided to have my links endpoint return an array with many different link type object types in it. Having a different URL per object type would have been simplier and more RESTful, however, this trade-off for complexity allows the UI to get all of the data that it needs in one API call. In reality, these architectural trade-offs would have been discussed and documented between affected groups.
* I chose a REST style API over GraphQL because it is what I am familiar with. GraphQL could be a more appropriate technology for this kind of problem.
* I wrote a lot of integration tests rather than unit tests. Unit testing controllers in ASP.NET is finicky, so I made the trade-off to get the project done faster. If this was a long running project, I'd shift most of the integration tests over to be unit tests.

## To do
* Add logging for priviledged actions like modifying data.
* Authorisation. Anyone can do anything right now.
* Use audit tables to keep track of data changing over time
* Restrict venue name and location lengths

## Schema design
My default is to go for a relational database. Modern relational DBs are incredibly performant and ACID gives you so much for free which cuts down on application code.

I've gone for a table per link type. We could totally denormalise it if having normalised data became an issue, but let's keep things tidy for now.

### ClassicLink table
| Column name | Data type | Constraints         | Notes |
| ----------- | --------- | --------------      | ----- |
| userId      | int       | Primary foreign key | Having this column in the links table allows super quick reduction of the dataset for a multi tenanted database. I'm not a DB expert, but I've been told integers should be first choice for indexes. GUIDs allow higher insert throughput but come with their own issues. See my opening notes. |
| linkId      | int       | Primary key         | Not unique to the table which means they only need to be sequential on a per user basis. This reduces noisy neighbor link creation overhead. |
| createdUtc | timestamp  |                     |       |
| title      | text       |                     | Its length is limited in the application, no need to reduce future options for change here |
| url        | text       |                     | Its length is limited in the application, no need to reduce future options for change here |


### ShowsLink table
| Column name | Data type | Constraints         | Notes                         |
| ----------- | --------- | --------------      | ----------------------------- |
| userId      | int       | Primary foreign key | Same reasoning as ClassicLink |
| linkId      | int       | Primary key         | Same reasoning as ClassicLink |
| createdUtc  | timestamp |                     |                               |
| title       | text      |                     | Same reasoning as ClassicLink |
| url         | text      |                     | Same reasoning as ClassicLink |

### ShowsLinkShows table
| Column name   | Data type | Constraints         | Notes               |
| ------------- | --------- | --------------      | ------------------- |
| userId        | int       | Primary foreign key | Ties this to an entry in ShowsLink |
| linkId        | int       | Primary foreign key | Ties this to an entry in ShowsLink |
| showId        | int       | Primary key         | Same reasoning as ClassicLink |
| date          | date      |                     |                     |
| venueName     | text      |                     |                     |
| venueLocation | text      |                     |                     |
| isSoldOut     | boolean   |                     |                     |
| isOnSale      | boolean   |                     |                     |

<p align="center">
  <img src="./Screen%20Shot%202019-07-08%20at%202.09.47%20pm.png">
</p>

# The Problem
We have three new link types for our users.

1. Classic
	- Titles can be no longer than 144 characters.
	- Some URLs will contain query parameters, some will not.
2. Shows List
	- One show will be sold out.
	- One show is not yet on sale.
	- The rest of the shows are on sale.
3. Music Player
	- Clients will need to link off to each individual platform.
	- Clients will embed audio players from each individual platform.
	
You are required to create a JSON API that our front end clients will interact with.

- The API can be GraphQL or REST.
- The API can be written in your preferred language.
- The client must be able to create a new link of each type.
- The client must be able to find all links matching a particular userId.
- The client must be able to find links matching a particular userId, sorted by dateCreated.


## Your Solution

- Consider bad input data and the end user of your API - we're looking for good error handling and input validation.
- If you are creating a GraphQL API, think about the access patterns the client may use, and think about the acces patterns the client may not use. Try not to [Yak Shave](https://seths.blog/2005/03/dont_shave_that/)
- Consider extensibility, these are 3 of hundreds of potential link types that we will be developing.


## Rules & Tips

- Choose the language and environment of your choice, just include documentation on how to run your code.
- Immutability and functional programming is looked upon favorably.
- You cannot connect to a real world database - document your schema design.
- Mocking third parties is looked upon favorably.
- @todo comments are encouraged. You aren't expected to complete the challenge, but how you design your solution and your ideas for the future are important.

---
# Submission
Set up your own remote git repository and make commits as you would in your day to day work. Submit a link to your repo when you're finished.
