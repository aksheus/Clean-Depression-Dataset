import os
import sys
from collections import defaultdict 
import numpy as np

join = lambda u,v: os.path.join(u,v)
isfile = lambda z : os.path.isfile(z)
isdir = lambda z : os.path.isdir(z)

if __name__ == '__main__':

	if isdir(sys.argv[1]):
		target_dir = sys.argv[1]
	else:
		print('invalid path to directory')
		exit(1)

	V = defaultdict(lambda: 0,{})

	exclude = set(['chunk1-2','chunk1-2-3','chunk1-2-3-4','chunk1-2-3-4-5','chunk1-2-3-4-5-6','chunk1-2-3-4-5-6-7','chunk1-2-3-4-5-6-7-8',
		'chunk1-2-3-4-5-6-7-8-9','chunk1-2-3-4-5-6-7-8-9-10'])

	meta_dirs = ['C:\\Users\\abkma\\nlp\\reddit-depression\\cleaned-train\\cleaned-pos','C:\\Users\\abkma\\nlp\\reddit-depression\\cleaned-train\\cleaned-neg',
	'C:\\Users\\abkma\\nlp\\reddit-depression\\cleaned-test']

	for target in meta_dirs:
		
		dirs = ( d for d in os.listdir(target) if isdir(join(target,d)))

		for d in dirs:
			if d not in exclude:
				files = ( f for f in os.listdir(join(target,d)) if isfile(join(join(target,d),f)))
				for f in files:
					with open(join(join(target,d),f),errors='ignore') as fh:
						for line in fh:
							if line.strip():
								for c in line.split():
									V[c]+=1

	freqs = []
	print('Frequency distribution of corpus is :')
	print('token	freq')
	for key in V.keys():
		print('{}	{}'.format(key,V[key]))
		freqs.append(V[key])
	del V
	print('Mean freq :',np.mean(freqs))
	print('Standard dev:',np.std(freqs))








