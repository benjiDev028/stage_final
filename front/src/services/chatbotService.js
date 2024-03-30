// chatService.js
const sendMessage = async (userInput) => {
    const response = await fetch('http://127.0.0.1:5000/chatbot', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ userInput }),
    });
  
    if (!response.ok) {
      throw new Error('Réponse du serveur non OK');
    }
  
    const data = await response.json();
    return data.response; // Retourner le texte de réponse pour être affiché
  };
  
  export { sendMessage };


  const Api_Url = "http://localhost:5000";

  async function analyzeApi(mot_cle, nbPhrases) {
    try {
      const response = await fetch(`${Api_Url}/analyze_google`, {  // Assurez-vous que l'URL correspond à celle de votre back-end
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          keyword: mot_cle,  // Le nom de cette propriété doit correspondre à ce que votre back-end attend
          num_sentences: nbPhrases  // De même ici
        })
      });
  
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
  
      const data = await response.json(); // Récupération de la réponse JSON du back-end
  
      return data; // Retour des données pour un traitement ultérieur
  
    } catch (error) {
      console.error("There was an error!", error);
    }
  }
  
  export{analyzeApi};


  
  async function modelApi(mot_cle, nbPhrases) {
    try {
      const response = await fetch(`${Api_Url}/analyze_sentiment`, {  // Assurez-vous que l'URL correspond à celle de votre back-end
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          keyword: mot_cle,  // Le nom de cette propriété doit correspondre à ce que votre back-end attend
          num_sentences: nbPhrases  // De même ici
        })
      });
  
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
  
      const data = await response.json(); // Récupération de la réponse JSON du back-end
  
      return data; // Retour des données pour un traitement ultérieur
  
    } catch (error) {
      console.error("There was an error!", error);
    }
  }
  
  export{modelApi};