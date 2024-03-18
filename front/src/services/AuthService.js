// AuthService.js

import { jwtDecode } from "jwt-decode";


// Fonction pour obtenir l'ID de l'utilisateur à partir du token JWT
function getUserIdFromToken(token) {
    
    try {
        const decodedToken = jwtDecode(token); // Utilisez jwtDecode ici
        const userId = decodedToken.sub; // 'sub' est généralement utilisé pour stocker l'identifiant de l'utilisateur dans un token JWT
        return userId;
    } catch (error) {
        console.error("Erreur lors du décodage du token :", error);
        return null;
    }
}

// Fonction pour vérifier si l'utilisateur est connecté
function isLoggedIn() {
    let token = localStorage.getItem("token");
    return token ? true : false;
}

// Export des fonctions
export { getUserIdFromToken, isLoggedIn };




const Api_Url = "http://localhost:5205/auth";

async function loginUser(email, password) {
    try {
        const response = await fetch(`${Api_Url}/connection`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ email, password })
        });

        if (!response.ok) {
            const errorMessage = await response.text();
            alert(errorMessage); // Affichez simplement le message d'erreur
            throw new Error(errorMessage);
        }

        const data = await response.json();
        const token = data;

        // Stockez le token dans localStorage ou sessionStorage
        localStorage.setItem('token',token);
        console.log(token) // Vous pouvez ajuster selon votre choix de stockage
        return token;
    } catch (error) {
        console.error(error.message);
        throw error;
    }
}
async function RegisterUser(firstName, lastName, email, username, password){
    try {
        const response = await fetch(`${Api_Url}/register`, {
            method: "POST",
            headers: {
                'Content-Type': "application/json", // Corrected header
            },
            body: JSON.stringify({ firstName, lastName, email, username, password })
        }); 
        
        if (!response.ok) {
            const errorDetails = await response.text(); // Assuming error details are in text form
            throw new Error(errorDetails || 'Unknown error occurred');
        }
         
        return await response.json();
    } catch(error) {
        console.log(error);
         // Rethrow to handle it in the calling function
    }
}




            

export { loginUser,RegisterUser };
