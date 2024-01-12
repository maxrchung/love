// 0 White
// 1 Red
// 2 Sun
// 3 Dark

const timings = [
  2841, 2855, 2877, 2883, 2890, 2905, 2913, 2930, 2941, 2951, 2962, 2966,
  2984, 2993, 3001, 3004, 3013, 3017, 3022, 3030, 3033, 3041, 3046, 3051, 3061,
  3076, 3082, 3087, 3097, 3108, 3118, 3128, 3134, 3148, 3166, 3173, 3178, 3191,
  3199, 3230, 3241, 3246, 3252, 3257, 3262, 3285, 3291, 3299, 3302, 3304, 3312,
  3326,
];

const getColorOutput = (color) => {
  switch (color) {
    case 0:
      return "Colors.White";
    case 1:
      return "Colors.Red";
    case 2:
      return "Colors.Sun";
    case 3:
      return "Colors.Dark";
  }
};

const getOutput = (color, index) => {
  return `            Framing.New(${timings[index]}, ${getColorOutput(color)}),`;
}

let prevBack = -1;
let prevFront = -1;

const includes = (colors, color) => {
  if (colors.includes(color)) {
    return true;
  }
  return false;
};

const areBothOrange = (back, front) => {
  if (back === 2 && front === 3) {
    return true;
  }

  if (back === 3 && front === 2) {
    return true;
  }

  return false;
};

const getColor = () => {
  return Math.floor(Math.random() * 4);
};

const backs = [];
const fronts = [];

for (let i = 0; i < timings.length; ++i) {
  let back = getColor();
  let front = getColor();

  while (includes([prevBack], back)) {
    back = getColor();
  }

  while (
    includes([prevFront, back], front)
  ) {
    front = getColor();
  }

  backs.push(back);
  fronts.push(front);

  prevBack = back;
  prevFront = front;
}

console.log("back");
for (var i = 0; i < backs.length; ++i) {
  console.log(getOutput(backs[i], i));
}

console.log("---------------------------------------------------------------");
console.log("---------------------------------------------------------------");
console.log("---------------------------------------------------------------");
console.log("---------------------------------------------------------------");
console.log("---------------------------------------------------------------");


console.log("front");
for (var i = 0; i < fronts.length; ++i) {
  console.log(getOutput(fronts[i], i));
}
