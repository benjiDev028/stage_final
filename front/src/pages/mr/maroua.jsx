import React, { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css'; 
import './maroua.css'

const ToggleSwitch = ({ id, name, checked, onChange }) => (
  <div className="form-check form-switch">
    <input
      className="form-check-input"
      type="checkbox"
      id={id}
      checked={checked}
      onChange={onChange}
    />
    <label className="form-check-label" htmlFor={id}>
      {name}
    </label>
  </div>
);

const toggleData = [
  { id: 'toggle1', name: 'Toggle 1', position: 'left' },
  { id: 'toggle2', name: 'Toggle 2', position: 'left' },
  { id: 'toggle3', name: 'Toggle 3', position: 'left' },

  { id: 'toggle4', name: 'Toggle 4', position: 'right' },
  { id: 'toggle5', name: 'Toggle 5', position: 'right' },
  { id: 'toggle6', name: 'Toggle 6', position: 'right' },

];

const Maroua = () => {
  const [toggleStates, setToggleStates] = useState(
    toggleData.reduce((states, toggle) => ({ ...states, [toggle.id]: false }), {})
  );

  const handleToggle = (id) => {
    setToggleStates({ ...toggleStates, [id]: !toggleStates[id] });
  };

  const handleSubmit = () => {
    console.log('Toggle states:', toggleStates);
    const activeTogglesCount = Object.values(toggleStates).filter(isActive => isActive).length;
    alert(`Nombre de toggles actifs : ${activeTogglesCount}`);
  };

  return (
    <div className="container-fluid">
      <h1 className="text-center mb-4">Titre de la page</h1>
      <div className="row">
        <div className="col d-flex justify-content-start">
          <div>
            <h2>Titre Gauche</h2>
            {toggleData.filter(t => t.position === 'left').map(toggle => (
              <ToggleSwitch
                key={toggle.id}
                id={toggle.id}
                name={toggle.name}
                checked={toggleStates[toggle.id]}
                onChange={() => handleToggle(toggle.id)}
              />
            ))}
          </div>
        </div>
        <div className="col d-flex justify-content-end align-items-start flex-column">
          <div>
            <h2>Titre Droit</h2>
            {toggleData.filter(t => t.position === 'right').map(toggle => (
              <ToggleSwitch
                key={toggle.id}
                id={toggle.id}
                name={toggle.name}
                checked={toggleStates[toggle.id]}
                onChange={() => handleToggle(toggle.id)}
              />
            ))}
          </div>
          <button className="btn btn-primary mt-auto" onClick={handleSubmit}>
            Valider
          </button>
        </div>
      </div>
    </div>
  );
};

export default Maroua;
