counts = Hash.new(0)

100.times do
  output = `curl -s http://20.67.185.247/pet`
  counts[output] += 1
end

puts counts