import React, { useEffect, useState } from "react";
import {
  fetchBoardGames,
  addBoardGame,
  updateBoardGame,
  deleteBoardGame,
  BoardGame,
} from "../services/boardGameService";

const BoardGameList: React.FC = () => {
  const [boardGames, setBoardGames] = useState<BoardGame[]>([]);
  const [selectedGameId, setSelectedGameId] = useState<number | null>(null);
  const [editGame, setEditGame] = useState<BoardGame | null>(null);
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

  const handleUpdateClick = (game: BoardGame) => {
    setEditGame({ ...game });
  };

  const handleUpdateConfirm = async () => {
    if (!editGame) return;
    await updateBoardGame(editGame);
    setEditGame(null);
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
              {editGame && editGame.id === game.id ? (
                <>
                  <input
                    type="text"
                    className="form-control mb-2"
                    value={editGame.name}
                    onChange={(e) =>
                      setEditGame({ ...editGame, name: e.target.value })
                    }
                  />
                  <input
                    type="text"
                    className="form-control mb-2"
                    value={editGame.description}
                    onChange={(e) =>
                      setEditGame({ ...editGame, description: e.target.value })
                    }
                  />
                  <button
                    className="btn btn-success btn-sm me-2"
                    onClick={handleUpdateConfirm}
                  >
                    OK
                  </button>
                  <button
                    className="btn btn-secondary btn-sm"
                    onClick={() => setEditGame(null)}
                  >
                    Cancel
                  </button>
                </>
              ) : (
                <>
                  <h5 className="mb-1">{game.name}</h5>
                  <p className="mb-1">{game.description}</p>
                  <button
                    className="btn btn-warning btn-sm me-2"
                    onClick={() => handleUpdateClick(game)}
                  >
                    Update
                  </button>
                  <button
                    className="btn btn-danger btn-sm"
                    onClick={() => handleDeleteGame(game.id)}
                  >
                    Delete
                  </button>
                </>
              )}
            </div>
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
