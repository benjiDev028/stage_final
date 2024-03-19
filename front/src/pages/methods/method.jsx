import React from 'react';
import { useNavigate } from 'react-router-dom';

const Method = () => {
  const navigate = useNavigate();

  const sentimentMethods = [
    { id: 'internal_model', name: 'Modèle interne', image: 'internal_model.jpg' },
    { id: 'azure_api', name: 'API Azure', image: 'azure_api.jpg' }
    // Ajoutez d'autres méthodes ici au besoin
  ];

  const redirectToMethodPage = (methodId) => {
    navigate(`/method/${methodId}`);
  };

  return (
    <div className="container mt-5">
      <h1 className="mb-4 text-center">Page d'analyse de sentiments</h1>
      <div className="row">
        {sentimentMethods.map(method => (
          <div key={method.id} className="col-md-6 col-lg-4 mb-4">
            <div className="card h-100">
              <img src={method.image} className="card-img-top" alt={method.name} />
              <div className="card-body">
                <h5 className="card-title text-center">{method.name}</h5>
              </div>
              <div className="card-footer d-flex justify-content-center">
                <button className="btn btn-primary" onClick={() => redirectToMethodPage(method.id)}>En savoir plus</button>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Method;
