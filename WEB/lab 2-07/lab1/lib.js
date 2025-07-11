const computeB = (x, y, z) => {return x + (Math.cbrt(z * y)) / (y + Math.cos(x));};

module.exports = { computeB };