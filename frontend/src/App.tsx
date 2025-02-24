import React from "react";
import BoardGamesList from "./components/BoardGamesList";

const App: React.FC = () => {
  return (
    <div>
      <h1>Board Game Collection</h1>
      <BoardGamesList />
    </div>
  );
};

export default App;
