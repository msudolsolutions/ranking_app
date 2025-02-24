import React, { useEffect, useState } from "react";
import {
  fetchBoardGames,
  addBoardGame,
  deleteBoardGame,
  BoardGame,
} from "../services/boardGameService";

const BoardGameList: React.FC = () => {
  const [boardGames, setBoardGames] = useState<BoardGame[]>([]);
  const [selectedGameId, setSelectedGameId] = useState<number | null>(null);
  const [newGame, setNewGame] = useState<BoardGame>({
    id: 0,
    name: "",
    description: "",
    imageId: 0,
    year: 0,
    minPlayers: 0,
    maxPlayers: 0,
    minAge: 0,
    duration: 0,
    difficulty: 0,
    rating: 0,
    itemType: 0,
  });

  useEffect(() => {
    loadBoardGames();
  }, []);

  const loadBoardGames = async () => {
    const games = await fetchBoardGames();
    setBoardGames(games);
  };

  const handleAddGame = async () => {
    if (!newGame.name) return;
    await addBoardGame(newGame);
    setNewGame({ ...newGame, name: "", description: "" });
    loadBoardGames();
  };

  const handleDeleteGame = async (id: number) => {
    await deleteBoardGame(id);
    loadBoardGames();
  };

  return (
    <div className="container mt-4">
      <h2 className="mb-3">Board Games</h2>
      <ul className="list-group mb-3">
        {boardGames.map((game) => (
          <li
            key={game.id}
            className={`list-group-item d-flex justify-content-between align-items-center ${
              selectedGameId === game.id ? "active" : ""
            }`}
            onClick={() => setSelectedGameId(game.id)}
            style={{ cursor: "pointer" }}
          >
            <div>
              <h5 className="mb-1">{game.name}</h5>
              <p className="mb-1">{game.description}</p>
            </div>
            <button
              className="btn btn-danger btn-sm"
              onClick={() => handleDeleteGame(game.id)}
            >
              Delete
            </button>
          </li>
        ))}
      </ul>

      <h3>Add New Game</h3>
      <div className="mb-3">
        <input
          type="text"
          className="form-control mb-2"
          placeholder="Name"
          value={newGame.name}
          onChange={(e) => setNewGame({ ...newGame, name: e.target.value })}
        />
        <input
          type="text"
          className="form-control mb-2"
          placeholder="Description"
          value={newGame.description}
          onChange={(e) =>
            setNewGame({ ...newGame, description: e.target.value })
          }
        />
        <button className="btn btn-primary" onClick={handleAddGame}>
          Add Game
        </button>
      </div>
    </div>
  );
};

export default BoardGameList;
