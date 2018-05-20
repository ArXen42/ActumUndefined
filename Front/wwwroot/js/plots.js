function LineChart(append_to) {
  var that = {};
  var parseTime = d3.timeParse("%d-%b-%y");
  that.margin = {top: 20, right: 20, bottom: 30, left: 50},
  that.width = 960 - that.margin.left - that.margin.right,
  that.height = 500 - that.margin.top - that.margin.bottom,
  that.xSelector = that.margin.left + that.width;
  that.xScale = d3.scaleTime().range([0, that.width]);
  that.yScale = d3.scaleLinear().range([that.height, 0]);
  that.line = d3.line()
  .x(function(d) { return that.xScale(d.year); })
  .y(function(d) { return that.yScale(d.avg); });
  that.svg = d3.select(append_to).append("svg")
    .attr("width", "100%")
    .attr("height", "100%")
    .attr("viewBox","0 0 960 500")
    .attr("preserveAspectRatio","none")
    .append("g")
  .attr("transform", "translate(" + that.margin.left + "," + that.margin.top + ")");
  that.updateLineChart = function (data) {
    data.forEach(function(d) {
      d.year = d.year;
      d.avg = +d.avg;
    });
    that.xScale.domain(d3.extent(data, function(d) { return d.year; }));
    that.yScale.domain([0,d3.max(data, function(d) {return d.avg;})]);

    that.svg.select(".line")
    .data([data])
    .attr("class", "line")
    .attr("d", that.line);
  };
  $.getJSON("http://10.20.0.76:5000/api/temperatures/global/period/1750/2015",
  function (data) {
    data.forEach(function(d) {
      d.year = d.year;
      d.avg = +d.avg;
    });
  that.xScale.domain(d3.extent(data, function(d) { return d.year; }));
  that.yScale.domain([0, d3.max(data, function(d) { return d.avg; })]);

  that.svg.append("g")
  .attr("class", "x axis")
  .attr("transform", "translate(0," + that.height + ")")
  .call(d3.axisBottom(that.xScale));
  that.svg.append("path")
    .data([data])
    .attr("class", "line")
    .attr("d", that.line);
  that.svg.append("g")
    .attr("transform", "translate(0," + that.height + ")")
    .call(d3.axisBottom(that.xScale));
  that.svg.append("g")
    .call(d3.axisLeft(that.yScale));
  });
  return that;
};
function BarChart(append_to,data_source){
  var that = {};
  that.margin = {top: 20, right: 20, bottom: 30, left: 40},
      that.width = 960 - that.margin.left - that.margin.right,
      that.height = 500 - that.margin.top - that.margin.bottom;

  // set the ranges
  that.x = d3.scaleBand()
            .range([0, that.width])
            .padding(0.1);
  that.y = d3.scaleLinear()
            .range([that.height, 0]);
  that.svg = d3.select(append_to).append("svg")
      .attr("width", "100%")
      .attr("height", "100%")
      .attr("viewBox","0 0 960 500")
      .attr("preserveAspectRatio","none")
    .append("g")
      .attr("transform", 
            "translate(" + that.margin.left + "," + that.margin.top + ")");
  var data = [{season:'Зима',value:0}, {season:'Весна',value:0}, {season:'Лето',value:0}, {season:'Осень',value:0}];
  for (var i = 0; i < 4; i++) {
    data[i].value = data_source[i];
  }
  // Scale the range of the data in the domains
  that.x.domain(data.map(function(d) { return d.season; }));
  that.y.domain([0, d3.max(data, function(d) { return d.value; })]);
  // append the rectangles for the bar chart
  that.svg.selectAll(".bar")
      .data(data)
    .enter().append("rect")
      .attr("class", "bar")
      .attr("x", function(d) { return that.x(d.season); })
      .attr("width", that.x.bandwidth())
      .attr("y", function(d) { return that.y(d.value); })
      .attr("height", function(d) { return that.height - that.y(d.value); });
  that.svg.append("g")
      .attr("transform", "translate(0," + that.height + ")")
      .call(d3.axisBottom(that.x));
  that.svg.append("g")
      .call(d3.axisLeft(that.y));
  return that;
};