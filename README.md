# Github User Activity CLI
A CLI tool to fetch and display GitHub user activity through the official GitHub API.

This was built using C# and .NET

## Features

- Fetches recent public events through a GitHub username
- Aggregates the activities by event type
- Displays results based on how many times the event has occured
- No external libraries used

## Example Output
```
PushEvent: 12
WatchEvent: 5
IssuesEvent: 3
ForkEvent: 1
IssueCommentEvent: 1
```

## How It Works

1. Takes a GitHub username as a command-line argument
2. Sends a request to GitHub's Events API: `https://api.github.com/users/{username}/events/public`
3. Parses the JSON response to extract event types
4. Aggregates events by type and counts occurrences
5. Displays results sorted in desc order (most common first)

## Error Handling

The application handles:
- Missing command-line arguments
- Invalid usernames (404 errors)
- Network failures
- JSON parsing errors
- General HTTP request exceptions

### This project was done in order to practice and demonstrate the following:
- HTTP API usage in C#
- JSON Deserialization
- LINQ usage in data aggregation
- Error handling and exception management