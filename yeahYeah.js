// 0 White
// 1 Red
// 2 Sun
// 3 Dark

const timings = [
  2840, 2854, 2876, 2882, 2890, 2904, 2911, 2930, 2941, 2951, 2962, 2966,
  2983, 2993, 3000, 3004, 3013, 3016, 3022, 3030, 3033, 3040, 3045, 3051, 3061,
  3076, 3082, 3086, 3097, 3107, 3112, 3127, 3133, 3147, 3165, 3173, 3177, 3190,
  3200, 3229, 3241, 3246, 3252, 3257, 3262, 3284, 3291, 3299, 3301, 3304, 3312,
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
