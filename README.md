# Ball-B: Multiplayer Ball Game

A containerized multiplayer ball game platform built with Unity and Go, featuring both public and private match capabilities.

![](https://github.com/batuhannoz/ball-b/blob/main/pictures/LobbyScreenshot.png)

![](https://github.com/batuhannoz/ball-b/blob/main/pictures/GameScreenshot.png)

## 🎮 Features

- **Multiplayer Gaming**: Real-time multiplayer ball game experience
- **Match Types**:
  - Public matches for quick play
  - Private matches for playing with friends
- **Docker Integration**: Each game instance runs in its own container
- **Dynamic Port Allocation**: Automatic port management for game instances
- **Match Management**: Create, join, and end game sessions

## 🛠 Technology Stack

- **Frontend**: Unity Game Engine
- **Backend**: Go (Fiber framework)
- **Containerization**: Docker
- **Networking**: WebSocket for real-time communication

## 🚀 Getting Started

### Prerequisites

- Docker
- Go 1.x
- Unity (for development)

### Installation

1. Clone the repository:
```bash
git clone https://github.com/yourusername/ball-b.git
cd ball-b
```

2. Build the backend:
```bash
cd backend
go mod download
go build
```

3. Build the game container:
```bash
docker build -t ball-b-game .
```

## 🎯 Usage

### Starting the Backend Server

```bash
cd backend
go run main.go
```

### Playing the Game

1. **Host a Public Match**:
   - Access the game client
   - Select "Host Public Match"
   - Wait for other players

2. **Host a Private Match**:
   - Select "Host Private Match"
   - Share the match ID with friends

3. **Join a Match**:
   - Use "Quick Match" for public games
   - Enter match ID for private games

## 🏗 Project Structure

```
ball-b/
├── backend/           # Go backend server
│   ├── main.go       # Server implementation
│   └── Dockerfile    # Backend container configuration
├── game/             # Unity game files
├── pictures/         # Game assets
└── Dockerfile        # Game container configuration
```

### Open Docker Permissions To All Users

```bash
sudo chmod 666 /var/run/docker.sock
```

#### Build Game Server

/builds/linux_server
```bash
docker build -t ball2d .
```

#### Build Backend

/backend
```bash
docker build -t backend .
```

#### Run Backend
```bash
docker run -d -p 3000:3000 backend
```

#### To Delete Running Containers
```bash
docker rm -f $(docker ps -a -q)
```

#### Send Game Files To Server
```bash
put -R /home/batuhan/Desktop/ball-b/builds/linux_server .
```
