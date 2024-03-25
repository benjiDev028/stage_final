const Api_Url ="http://localhost:5086"


async function postReview(idAI, idUser, content, nombreEtoile) {
    try {
        const response = await fetch(`${Api_Url}/review/post`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                idAI, 
                idUser, 
                content, // Make sure this matches 'content' exactly as in Postman
                nombreEtoile: parseInt(nombreEtoile, 10) // Parse nombreEtoile as an integer
            })
        });

        if (!response.ok) {
            const errorDetails = await response.json();
            throw new Error(`Error: ${errorDetails.title} - ${JSON.stringify(errorDetails.errors)}`);
        }
     
        return await response.json();
    } catch (error) {
        console.error('Error during review submission:', error.message);
        throw error;
    }
}
async function getReviews() {
    try {
        const response = await fetch(`${Api_Url}/review/all`, {
            method: 'GET'
        });

        if (!response.ok) {
            throw new Error('Failed to fetch reviews');
        }

        const data = await response.json();
        
        // Récupérer les noms d'utilisateur associés aux IDs d'utilisateur dans chaque commentaire
        const commentairesWithUsername = await Promise.all(data.map(async (commentaire) => {
            try {
                const userResponse = await fetch(`${Api_Url}/users/GetById/${commentaire.idUser}`, {
                    method: 'GET'
                });

                if (!userResponse.ok) {
                    throw new Error('Failed to fetch user');
                }

                const user = await userResponse.json();
                return { ...commentaire, username: user.username }; // Ajouter le nom d'utilisateur à chaque commentaire
            } catch (error) {
                console.error('Error fetching user:', error);
                // Si une erreur se produit lors de la récupération de l'utilisateur, retourner le commentaire sans nom d'utilisateur
                return commentaire;
            }
        }));

        return commentairesWithUsername;
    } catch (error) {
        console.error('Error fetching reviews:', error);
        throw error; // Re-throwing the error for handling in the calling code
    }
}


export {postReview,getReviews};