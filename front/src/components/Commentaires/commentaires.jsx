import React, { useState, useEffect } from 'react';
import { Card, ListGroup } from 'react-bootstrap';
import { getReviews } from '../../services/reviewService'; // Importez la fonction getReviews depuis votre fichier api.js

function Commentaires() {
    const [commentaires, setCommentaires] = useState([]);

    useEffect(() => {
        async function fetchReviews() {
            try {
                const reviews = await getReviews(); // Utilisez la fonction getReviews pour récupérer les commentaires
                setCommentaires(reviews);
            } catch (error) {
                console.error('Error fetching reviews:', error);
            }
        }
        fetchReviews();
    }, []);

    return (
        <div className="container">
            <div className="row">
                <div className="col-md-12 text-center">
                    <h2>Commentaires</h2>
                </div>
                {commentaires.slice(0, 4).map((commentaire, index) => (
                    <div className="col-md-6" key={index}>
                        <Card>
                            <Card.Body>
                                <Card.Title>{commentaire.username}</Card.Title>
                                <Card.Text>{commentaire.content}</Card.Text>
                            </Card.Body>
                            <ListGroup className="list-group-flush">
                                <ListGroup.Item>Nombre d'étoiles : {commentaire.nombreEtoile}</ListGroup.Item>
                                <ListGroup.Item>Date de publication : {new Date(commentaire.datepublication).toLocaleDateString()}</ListGroup.Item>
                            </ListGroup>
                        </Card>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default Commentaires;
