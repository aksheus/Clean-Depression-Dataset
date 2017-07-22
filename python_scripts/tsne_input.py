"""
USAGE : python tsne_input.py labeled_arff_file
"""
import sys
import os

if __name__=='__main__':

	if os.path.isfile(sys.argv[1]):
		arff_file=sys.argv[1]
	else:
		print('arff file does not exist')
		exit(1)

	with open(arff_file) as af:
		data_flag = False
		with open('mnist2500_labels.txt','w') as out_labels:
			with open('mnist2500_X.txt','w') as out_vectors:
				for line in af:

					pieces = line.split()

					if data_flag:

						out_labels.write(pieces[-2])
						out_labels.write('\n')

						out_vectors.write('   '.join(s[:-1] for s in pieces[:-2]))
						out_vectors.write('\n')

					if pieces[0] == '@data':
						data_flag = True
