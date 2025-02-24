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
    <div>
      <h2>Board Games</h2>
      <ul>
        {boardGames.map((game) => (
          <li key={game.id}>
            <h3>{game.name}</h3>
            <p>{game.description}</p>
            <button onClick={() => handleDeleteGame(game.id)}>Delete</button>
          </li>
        ))}
      </ul>

      <h3>Add New Game</h3>
      <input
        type="text"
        placeholder="Name"
        value={newGame.name}
        onChange={(e) => setNewGame({ ...newGame, name: e.target.value })}
      />
      <input
        type="text"
        placeholder="Description"
        value={newGame.description}
        onChange={(e) =>
          setNewGame({ ...newGame, description: e.target.value })
        }
      />
      <button onClick={handleAddGame}>Add Game</button>
    </div>
  );
};

export default BoardGameList;
