// Home.js
import React,{useEffect,useState} from 'react';
import '../Home/home.css';
import { Card, ListGroup } from 'react-bootstrap';
import { formatDistanceToNow } from 'date-fns';


import Header from '../../components/Header/Header';
import Footer from '../../components/Footer/footer';
import Internal from '../../components/Internal_model/internal';
import { getReviewsIa } from '../../services/reviewService';



function ModelP() {

  const [commentaires, setCommentaires] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [commentairesPerPage] = useState(4); // Nombre de commentaires par
  const idIA = "1";

  const refreshComments = async () => {
    try {
      const reviews = await getReviewsIa(idIA);
      setCommentaires(reviews);
    } catch (error) {
      console.error('Erreur lors de la récupération des commentaires:', error);
    }
  };

  // Rafraîchir les commentaires au montage du composant
  useEffect(() => {
    refreshComments();
  }, []);

 

  
  useEffect(() => {
    async function fetchReviews() {
      try {
        const reviews = await getReviewsIa(idIA); // Utilisez la fonction getReviews pour récupérer les commentaires
        setCommentaires(reviews);
      } catch (error) {
        console.error('Error fetching reviews:', error);
      }
    }

    fetchReviews();
  }, []);

    // Obtenir les commentaires actuels
    const indexOfLastCommentaire = currentPage * commentairesPerPage;
    const indexOfFirstCommentaire = indexOfLastCommentaire - commentairesPerPage;
    const currentCommentaires = commentaires.slice(indexOfFirstCommentaire, indexOfLastCommentaire);
  
    // Changer de page
    const paginate = (pageNumber) => setCurrentPage(pageNumber);
  
    // Calcul du nombre total de pages
    const pageCount = Math.ceil(commentaires.length / commentairesPerPage);
    const pages = Array.from({ length: pageCount }, (_, i) => i + 1);

  return (
    <div className="home">
     <Header/>
     <br/>
     <Internal onCommentAdded={refreshComments}/>
     
     <div className="row">
        <div className="col-md-12 text-center">
          <h2>Commentaires</h2>
        </div>
        {currentCommentaires.map((commentaire, index) => (
          <div className="col-md-6" key={index}>
            <Card>
              <Card.Body>
                <Card.Title>{commentaire.username}</Card.Title>
                <Card.Text>{commentaire.content}</Card.Text>
              </Card.Body>
              <ListGroup className="list-group-flush">
                <ListGroup.Item>Nombre d'étoiles : {commentaire.nombreEtoile}</ListGroup.Item>
                <ListGroup.Item>
                      Date de publication : {
                        formatDistanceToNow(new Date(commentaire.datepublication), { addSuffix: true, includeSeconds: true})
                      }
                </ListGroup.Item>
              </ListGroup>
            </Card>
          </div>
        ))}
      </div>
      <div className="pagination">
        {pages.map(number => (
          <button key={number} onClick={() => paginate(number)}>
            {number}
          </button>
        ))}
      </div>
     <br/>
     <Footer/>
    </div>
  );
}

export default ModelP;
