import React from 'react';

const ButtonComponent = ({ type, className, title }) => (
    <button type={ type } className={ className }>{ title }</button>
);

// Button propTypes (requirements)
ButtonComponent.propTypes = {
    type: React.PropTypes.string.isRequired,
    type: React.PropTypes.string.isRequired,
    title: React.PropTypes.string.isRequired
}

export default ButtonComponent;