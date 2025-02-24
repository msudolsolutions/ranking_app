import React, { useEffect, useState } from "react";
import { fetchBoardGames, BoardGame } from "../services/boardGameService";

const BoardGamesList: React.FC = () => {
  const [boardGames, setBoardGames] = useState<BoardGame[]>([]);

  useEffect(() => {
    fetchBoardGames().then(setBoardGames);
  }, []);

  return (
    <div>
      <h2>Board Games</h2>
      <ul>
        {boardGames.map((game) => (
          <li key={game.id}>
            <h3>{game.name}</h3>
            <p>{game.description}</p>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default BoardGamesList;
