export interface BoardGame {
    id: number;
    name: string;
    description: string;
    imageId: number;
    year: number;
    minPlayers: number;
    maxPlayers: number;
    minAge: number;
    duration: number;
    difficulty: number;
    rating: number;
    itemType: number;
  }
  
  const API_URL = "https://localhost:7197/api/BoardGame";
  
  export const fetchBoardGames = async (): Promise<BoardGame[]> => {
    const response = await fetch(API_URL);
    if (!response.ok) throw new Error("Failed to fetch board games");
    return await response.json();
  };
  
  export const addBoardGame = async (game: BoardGame): Promise<BoardGame> => {
    const response = await fetch(API_URL, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(game),
    });
    if (!response.ok) throw new Error("Failed to add board game");
    return await response.json();
  };
  
  export const updateBoardGame = async (game: BoardGame): Promise<void> => {
    const response = await fetch(`${API_URL}/${game.id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(game),
    });
    if (!response.ok) throw new Error("Failed to update board game");
  };
  
  export const deleteBoardGame = async (id: number): Promise<void> => {
    const response = await fetch(`${API_URL}/${id}`, {
      method: "DELETE",
    });
    if (!response.ok) throw new Error("Failed to delete board game");
  };
  