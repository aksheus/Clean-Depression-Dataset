"""
  pass directory for which relevant tags have to be collected 

  USAGE: python GetPosts.py <read direc> <write direc> <tag>

"""
import os
import sys
from bs4 import BeautifulSoup as BS 

join = lambda u,v: os.path.join(u,v)
isfile = lambda z : os.path.isfile(z)
isdir = lambda z : os.path.isdir(z)



if __name__ == '__main__':

	if isdir(sys.argv[1]):
		read_dir = sys.argv[1]
	else:
		print('Invalid read directory')

	if isdir(sys.argv[2]):
		write_dir = sys.argv[2]
	else:
		print('Invalid read directory')
	
	tag = sys.argv[3]
	files = ( f for f in os.listdir(read_dir) if isfile(join(read_dir,f)))
	for f in files:

		posts = []

		with open(join(read_dir,f),errors='replace') as fh:
			soup = BS(fh)
			posts = [ post.text for post in soup.find_all(tag)]

		to_write = '.'.join( s for s in f.split('.')[:-1]) + '.txt'

		with open(join(write_dir,to_write),'w',errors='replace') as out:
			for p in posts:
				out.write(p)
				out.write('\n')
				out.write('\n')
