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
    try {
      const response = await fetch(API_URL);
      if (!response.ok) throw new Error("Failed to fetch board games");
      return await response.json();
    } catch (error) {
      console.error(error);
      return [];
    }
  };
  