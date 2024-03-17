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
            alert(`Erreur lors de la connexion : ${errorMessage}`);
            throw new Error('Erreur lors de la connexion');
        }

        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Erreur lors de la connexion :', error.message);
        throw error;
    }
}

export { loginUser };
